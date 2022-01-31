import 'dart:convert';

import 'package:b2b/model/core/json_serializable.dart';

import 'municipio.dart';

class Estado extends JsonSerializable {
  final String uf;
  final List<Municipio> municipios;

  Estado(this.uf, this.municipios);

  Estado.fromJson(Map<String, dynamic> json)
      : uf = json['uf'],
        municipios = json['municipios'];

  @override
  fromJson(Map<String, dynamic> json) => Estado.fromJson(json);

  @override
  Map<String, dynamic> toJson() {
    // TODO: implement toJson
    throw UnimplementedError();
  }
}
