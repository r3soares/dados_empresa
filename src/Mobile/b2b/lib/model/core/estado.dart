import 'dart:convert';

import 'municipio.dart';

class Estado {
  final String uf;
  final List<Municipio> municipios;

  Estado(this.uf, this.municipios);

  Estado.fromJson(Map<String, dynamic> json)
      : uf = json['UF'],
        municipios = json['Municipios'];
}
