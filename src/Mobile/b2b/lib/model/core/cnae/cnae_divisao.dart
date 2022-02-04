import 'cnae_grupo.dart';

class CnaeDivisao {
  final int id;
  final String descricao;
  final String codSecao;
  final List<CnaeGrupo> grupos;

  CnaeDivisao(this.id, this.descricao, this.codSecao, this.grupos);

  CnaeDivisao.fromJson(Map<String, dynamic> json)
      : id = json['id'],
        descricao = json['descricao'],
        codSecao = json['codSecao'],
        grupos = json['grupos'] ?? List.empty(growable: true);

  Map<String, dynamic> toMap() => <String, dynamic>{
        'id': id,
        'descricao': descricao,
        'codSecao': codSecao,
        //'grupos': grupos,
      };
}
