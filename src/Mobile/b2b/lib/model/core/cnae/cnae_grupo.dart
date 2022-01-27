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
}
