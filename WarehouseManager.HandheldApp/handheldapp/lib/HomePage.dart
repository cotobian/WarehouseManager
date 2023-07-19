import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:handheldapp/main.dart';
import 'package:http/http.dart' as http;

class HomePage extends StatelessWidget {
  HomePage(this.jwt, this.payload);

  factory HomePage.fromBase64(String jwt) => HomePage(
      jwt,
      json.decode(
          ascii.decode(base64.decode(base64.normalize(jwt.split(".")[1])))));

  final String jwt;
  final Map<String, dynamic> payload;

  @override
  Widget build(BuildContext context) {
    var url = Uri.parse('$SERVER_IP/data');
    return Scaffold(
      appBar: AppBar(
        title: Text("Secret Data Screen"),
      ),
      body: Center(
        child: FutureBuilder(
            future: http.read(url, headers: {"Authorization": jwt}),
            builder: (context, snapshot) => snapshot.hasData
                ? Column(
                    children: <Widget>[
                      Text("${payload['username']}, here's the data:"),
                      //Text(snapshot.data, style: Theme.of(context).textTheme.displayMedium,)
                    ],
                  )
                : snapshot.hasError
                    ? Text("An error occurred")
                    : CircularProgressIndicator()),
      ),
    );
  }
}
