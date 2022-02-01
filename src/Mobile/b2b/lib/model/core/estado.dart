import 'package:b2b/utils/services/local_storage_service.dart';
import 'package:sqflite/sqflite.dart';
import 'package:sqflite_common/sqlite_api.dart';

import 'municipio.dart';

class Estado {
  final String uf;
  final List<Municipio> municipios;

  Estado(this.uf, this.municipios);

  Estado.fromJson(Map<String, dynamic> json)
      : uf = json['uf'],
        municipios = json['municipios'];

  Estado.fromMap(Map<dynamic, dynamic> json)
      : uf = json['uf'],
        municipios = List.empty(growable: true);

  Map<String, dynamic> toMap() => <String, dynamic>{
        'uf': uf,
        //'municipios': municipios,
      };
}
