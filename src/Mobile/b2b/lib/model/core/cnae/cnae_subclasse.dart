import '../empresa.dart';

/// <summary>
/// Seção -> Divisão -> Grupo -> Classe -> Subclasse -> Atividade Econômica
/// </summary>
class CnaeSubclasse {
  final int id;
  final String descricao;
  final int codClasse;
  final List<Empresa> empresas;

  CnaeSubclasse(this.id, this.descricao, this.codClasse, this.empresas);

  CnaeSubclasse.fromJson(Map<String, dynamic> json)
      : id = json['id'],
        descricao = json['descricao'],
        codClasse = json['codClasse'],
        empresas = json['empresas'] ?? List.empty(growable: true);

  Map<String, dynamic> toMap() => <String, dynamic>{
        'id': id,
        'descricao': descricao,
        'codClasse': codClasse,
        //'empresas': empresas,
      };
}
