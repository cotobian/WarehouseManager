import 'dart:convert';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:handheldapp/models/CreatePalletDetailVm.dart';
import 'package:handheldapp/models/CompleteForkliftJobVm.dart';
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

  //get all available PO
  static Future<List<String>> availablePO() async {
    final storage = FlutterSecureStorage();
    var url = Uri.parse('$baseUrl/ReceiptDetail/availablePO');
    final token = await storage.read(key: 'jwt');
    var headers = {'Content-Type': 'application/json'};
    if (token != null) {
      headers['Authorization'] = 'Bearer $token';
    }
    final response = await http.get(
      url,
      headers: headers,
    );
    if (response.statusCode == 200) {
      final data = json.decode(response.body);
      return List<String>.from(data);
    } else
      return [];
  }

  //get all available Item
  static Future<List<String>> availableItem() async {
    final storage = FlutterSecureStorage();
    var url = Uri.parse('$baseUrl/ReceiptDetail/availableItem');
    final token = await storage.read(key: 'jwt');
    var headers = {'Content-Type': 'application/json'};
    if (token != null) {
      headers['Authorization'] = 'Bearer $token';
    }
    final response = await http.get(
      url,
      headers: headers,
    );
    if (response.statusCode == 200) {
      final data = json.decode(response.body);
      return List<String>.from(data);
    } else
      return [];
  }

  //get remain quantity by po and item
  static Future<int> remainQuantity(String po, String item) async {
    final storage = FlutterSecureStorage();
    var url = Uri.parse('$baseUrl/ReceiptDetail/PO/$po/Item/$item');
    final token = await storage.read(key: 'jwt');
    var headers = {'Content-Type': 'application/json'};
    if (token != null) {
      headers['Authorization'] = 'Bearer $token';
    }
    final response = await http.get(
      url,
      headers: headers,
    );
    if (response.statusCode == 200) {
      final data = json.decode(response.body);
      return data as int;
    } else {
      return 0;
    }
  }

  //post pallet details
  static Future<bool> postDetails(List<CreatePalletDetailVm> list) async {
    final storage = FlutterSecureStorage();
    final url = Uri.parse('$baseUrl/PalletDetail/PostListVm');
    final token = await storage.read(key: 'jwt');
    var headers = {'Content-Type': 'application/json'};
    if (token != null) {
      headers['Authorization'] = 'Bearer $token';
    }
    final jsonData = list.map((item) => item.toJson()).toList();
    final response = await http.post(
      url,
      headers: headers,
      body: jsonEncode(jsonData),
    );
    if (response.statusCode == 200) {
      return true;
    } else {
      return false;
    }
  }

  //get forklift job pallet
  static Future<List<String>> getForkliftJob() async {
    final storage = FlutterSecureStorage();
    var url = Uri.parse('$baseUrl/ForkliftJob');
    final token = await storage.read(key: 'jwt');
    var headers = {'Content-Type': 'application/json'};
    if (token != null) {
      headers['Authorization'] = 'Bearer $token';
    }
    final response = await http.get(
      url,
      headers: headers,
    );
    if (response.statusCode == 200) {
      final data = json.decode(response.body);
      return List<String>.from(data);
    } else
      return [];
  }

  //complete forklift job
  static Future<bool> completeForkliftJob(CompleteForkliftJobVm jobVm) async {
    final storage = FlutterSecureStorage();
    final url = Uri.parse('$baseUrl/ForkliftJob/CompleteJob');
    final token = await storage.read(key: 'jwt');
    var headers = {'Content-Type': 'application/json'};
    if (token != null) {
      headers['Authorization'] = 'Bearer $token';
    }
    final response = await http.post(
      url,
      headers: headers,
      body: jsonEncode(jobVm),
    );
    if (response.statusCode == 200) {
      return true;
    } else {
      return false;
    }
  }
}
