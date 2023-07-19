import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:handheldapp/HomePage.dart';
import 'main.dart';
import 'package:http/http.dart' as http;

class LoginPage extends StatelessWidget {
  final TextEditingController _usernameController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();

  void displayDialog(context, title, text) => showDialog(
        context: context,
        builder: (context) => AlertDialog(
          title: Text(title),
          content: Text(text),
        ),
      );

  Future<String?> attempLogIn(String username, String password) async {
    var url = Uri.parse('$SERVER_IP/Auth/Login');
    var headers = {'Content-Type': 'application/json'};
    var res = await http.post(url,
        headers: headers,
        body: jsonEncode({"username": username, "password": password}));
    if (res.statusCode == 200) return res.body;
    return null;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text("Log In")),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: Column(
          children: <Widget>[
            TextField(
              controller: _usernameController,
              decoration: InputDecoration(labelText: 'Username'),
            ),
            TextField(
              controller: _passwordController,
              obscureText: true,
              decoration: InputDecoration(labelText: 'Password'),
            ),
            ElevatedButton(
                onPressed: () async {
                  var username = _usernameController.text;
                  var password = _passwordController.text;
                  var jwt = await attempLogIn(username, password);
                  if (jwt != null) {
                    storage.write(key: "jwt", value: jwt);
                    Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (context) => HomePage.fromBase64(jwt)));
                  } else {
                    displayDialog(context, "An error occured",
                        "No account was found matching username and password");
                  }
                },
                child: Text("Log In")),
          ],
        ),
      ),
    );
  }
}
