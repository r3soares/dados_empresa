import 'cnae_subclasse.dart';

class CnaeClasse {
  final int id;
  final String descricao;
  final int codGrupo;
  final List<CnaeSubclasse> subclasses;

  CnaeClasse(this.id, this.descricao, this.codGrupo, this.subclasses);

  CnaeClasse.fromJson(Map<String, dynamic> json)
      : id = json['id'],
        descricao = json['descricao'],
        codGrupo = json['codGrupo'],
        subclasses = json['subClasses'] ?? List.empty(growable: true);

  Map<String, dynamic> toMap() => <String, dynamic>{
        'id': id,
        'descricao': descricao,
        'codGrupo': codGrupo,
        //'subClasses': subClasses,
      };
}
