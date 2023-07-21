import 'package:flutter/material.dart';
import 'package:jwt_decoder/jwt_decoder.dart';
import 'ForkLift/HomePageLift.dart';
import 'Tally/HomePageTally.dart';
import 'User/HomePageUser.dart';
import 'main.dart';
import '../logics/ApiService.dart';

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

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
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
                  var jwt = await ApiService.logIn(
                    username,
                    password,
                  );
                  if (jwt != null) {
                    storage.write(
                      key: "jwt",
                      value: jwt,
                    );
                    Map<String, dynamic> decodedToken =
                        JwtDecoder.decode(jwt.toString());
                    String handheld = decodedToken['HandheldUser'];
                    if (handheld == 'Tally') {
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (context) => HomePageTally(),
                        ),
                      );
                    } else if (handheld == 'ForkLift') {
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (context) => HomePageLift(),
                        ),
                      );
                    } else {
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (context) => HomePageUser(),
                        ),
                      );
                    }
                  } else {
                    displayDialog(
                      context,
                      "An error occured",
                      "No account was found matching username and password",
                    );
                  }
                },
                child: Text("Log In"),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
