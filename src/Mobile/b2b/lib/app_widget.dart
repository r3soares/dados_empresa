import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';

class AppWidget extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      //scrollBehavior: CustomScrollBehavior(),
      debugShowCheckedModeBanner: false,
      title: 'B2B',
      theme: ThemeData(primarySwatch: Colors.blue, brightness: Brightness.light),
    ).modular();
  }
}
