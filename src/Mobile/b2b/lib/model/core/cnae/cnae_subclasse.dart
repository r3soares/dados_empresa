import '../empresa.dart';
import 'cnae_classe.dart';

/// <summary>
/// Seção -> Divisão -> Grupo -> Classe -> Subclasse -> Atividade Econômica
/// </summary>
class CnaeSubclasse {
  final int id;
  final String descricao;
  final int codClasse;
  final List<Empresa> empresas;

  CnaeSubclasse(this.id, this.descricao, this.codClasse, this.empresas);
}
