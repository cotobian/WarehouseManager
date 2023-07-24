import 'dart:convert';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;

class ApiService {
  static const String baseUrl = 'https://localhost:7122/api';
  //login user
  static Future<String?> logIn(String username, String password) async {
    var url = Uri.parse('$baseUrl/Auth/Login');
    var headers = {'Content-Type': 'application/json'};
    var res = await http.post(
      url,
      headers: headers,
      body: jsonEncode(
        {
          "username": username,
          "password": password,
        },
      ),
    );
    if (res.statusCode == 200) return res.body;
    return null;
  }

  //search PO by text
  static Future<List<String>> suggestPO(String po) async {
    final storage = FlutterSecureStorage();
    var url = Uri.parse('$baseUrl/ReceiptDetail/pocontains/$po');
    final token = await storage.read(key: 'jwt');
    var headers = {'Content-Type': 'application/json'};
    if (token != null) {
      headers['Authorization'] = 'Bearer $token';
    }
    final response = await http.get(url, headers: headers);
    if (response.statusCode == 200) {
      final data = json.decode(response.body);
      return List<String>.from(data);
    } else
      return [];
  }

  //get all available PO
  static Future<List<String>> availablePO() async {
    final storage = FlutterSecureStorage();
    var url = Uri.parse('$baseUrl/ReceiptDetail/availablePO');
    final token = await storage.read(key: 'jwt');
    var headers = {'Content-Type': 'application/json'};
    if (token != null) {
      headers['Authorization'] = 'Bearer $token';
    }
    final response = await http.get(url, headers: headers);
    if (response.statusCode == 200) {
      final data = json.decode(response.body);
      return List<String>.from(data);
    } else
      return [];
  }
}
