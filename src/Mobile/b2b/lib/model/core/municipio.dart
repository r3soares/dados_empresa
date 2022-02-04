import 'empresa.dart';

class Municipio {
  final int id;
  final String nome;
  final String uf;
  final List<Empresa> empresas;

  Municipio(this.id, this.nome, this.uf, this.empresas);

  Municipio.fromJson(Map<String, dynamic> json)
      : id = json['id'],
        nome = json['nome'],
        uf = json['uf'],
        empresas = json['empresas'] ?? List.empty(growable: true);

  Map<String, dynamic> toMap() => <String, dynamic>{
        'id': id,
        'nome': nome,
        'uf': uf,
        //'empresas': empresas,
      };
}
