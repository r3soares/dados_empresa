import 'cnae_classe.dart';
import 'cnae_divisao.dart';

/// <summary>
/// Seção -> Divisão -> Grupo -> Classe -> Subclasse -> Atividade Econômica
/// </summary>
class CnaeGrupo {
  final int id;
  final String descricao;
  //final IList<String> Observacoes { get;}
  final CnaeDivisao divisao;
  final List<CnaeClasse> classes;

  CnaeGrupo(this.id, this.descricao, this.divisao, this.classes);
}
