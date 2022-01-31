import 'package:b2b/model/core/json_serializable.dart';

import 'empresa.dart';

class Municipio extends JsonSerializable {
  final int id;
  final String nome;
  final String uf;
  final List<Empresa> empresas;

  Municipio(this.id, this.nome, this.uf, this.empresas);

  @override
  fromJson(Map<String, dynamic> json) => Municipio.fromJson(json);

  @override
  Map<String, dynamic> toJson() {
    // TODO: implement toJson
    throw UnimplementedError();
  }

  Municipio.fromJson(Map<String, dynamic> json)
      : id = json['id'],
        nome = json['nome'],
        uf = json['uf'],
        empresas = json['empresas'] ?? List.empty(growable: true);
}
