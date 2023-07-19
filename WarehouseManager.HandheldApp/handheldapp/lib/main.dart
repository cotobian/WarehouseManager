import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'MyApp.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'dart:convert' show json, base64, ascii, utf8;

const SERVER_IP = 'https://localhost:7122/api';
final storage = FlutterSecureStorage();

void main() {
  runApp(MyApp());
}
