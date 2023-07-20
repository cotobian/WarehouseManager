import 'dart:convert';
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
}
