import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';

class HomePage extends StatefulWidget {
  final String title;
  const HomePage({Key? key, this.title = "Home"}) : super(key: key);

  @override
  _HomePageState createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  @override
  Widget build(BuildContext context) {
    final size = MediaQuery.of(context).size;
    return Scaffold(
        // appBar: AppBar(
        //   leading: Icon(Icons.person),
        //   title: Text('Nome do Usuário'),
        //   actions: [
        //     ElevatedButton.icon(
        //         style: ButtonStyle(elevation: MaterialStateProperty.all(0)),
        //         onPressed: () => {},
        //         icon: Icon(Icons.exit_to_app),
        //         label: Text('Sair'))
        //   ],
        // ),
        body: Container(
      padding: EdgeInsets.only(bottom: size.height * .3),
      alignment: Alignment.center,
      child: const Center(
        child: Text('Menu'),
      ),
    ));
  }
}
