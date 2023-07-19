import 'package:flutter/material.dart';
import 'MyApp.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

const SERVER_IP = 'https://localhost:7122/api';
final storage = FlutterSecureStorage();

void main() {
  storage.deleteAll();
  runApp(MyApp());
}
