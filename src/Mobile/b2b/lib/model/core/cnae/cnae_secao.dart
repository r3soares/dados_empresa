import 'cnae_divisao.dart';

/// Seção -> Divisão -> Grupo -> Classe -> Subclasse -> Atividade Econômica
/// </summary>
class CnaeSecao {
  final String id;
  final String descricao;
  final List<CnaeDivisao> divisoes;

  CnaeSecao(this.id, this.descricao, this.divisoes);

  CnaeSecao.fromJson(Map<String, dynamic> json)
      : id = json['id'],
        descricao = json['descricao'],
        divisoes = json['divisoes'] ?? List.empty(growable: true);

  Map<String, dynamic> toMap() => <String, dynamic>{
        'id': id,
        'descricao': descricao,
        //'divisoes': divisoes,
      };
}
