import 'cnae_classe.dart';
import 'cnae_divisao.dart';

/// <summary>
/// Seção -> Divisão -> Grupo -> Classe -> Subclasse -> Atividade Econômica
/// </summary>
class CnaeGrupo {
  final int id;
  final String descricao;
  final int codDivisao;
  final List<CnaeClasse> classes;

  CnaeGrupo(this.id, this.descricao, this.codDivisao, this.classes);

  CnaeGrupo.fromJson(Map<String, dynamic> json)
      : id = json['id'],
        descricao = json['descricao'],
        codDivisao = json['codDivisao'],
        classes = json['classes'] ?? List.empty(growable: true);

  Map<String, dynamic> toMap() => <String, dynamic>{
        'id': id,
        'descricao': descricao,
        'codDivisao': codDivisao,
        //'classes': classes,
      };
}
