import 'package:flutter/material.dart';
import 'MyApp.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

final storage = FlutterSecureStorage();

void main() {
  storage.deleteAll();
  storage.write(
    key: "jwt_token_key",
    value: 'htmmanagementwarehouseandtruckingsystem',
  );
  runApp(MyApp());
}
