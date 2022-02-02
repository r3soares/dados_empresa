import 'municipio.dart';

class Estado {
  final String uf;
  final List<Municipio> municipios;

  Estado(this.uf, this.municipios);

  Estado.fromJson(Map<String, dynamic> json)
      : uf = json['uf'],
        municipios = json['municipios'] ?? List.empty();

  // Estado.fromMap(Map<dynamic, dynamic> map)
  //     : uf = map['uf'],
  //       municipios = List.empty(growable: true);

  Map<String, dynamic> toMap() => <String, dynamic>{
        'uf': uf,
        //'municipios': municipios,
      };
}
