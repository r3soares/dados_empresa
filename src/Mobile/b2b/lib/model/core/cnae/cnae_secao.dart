import 'cnae_divisao.dart';

/// Seção -> Divisão -> Grupo -> Classe -> Subclasse -> Atividade Econômica
/// </summary>
class Cnaesecao {
  final String id;
  final String descricao;
  final List<CnaeDivisao> divisoes;

  Cnaesecao(this.id, this.descricao, this.divisoes);
}
