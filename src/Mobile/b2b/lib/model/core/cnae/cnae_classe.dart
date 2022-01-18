import 'cnae_grupo.dart';
import 'cnae_subclasse.dart';

class CnaeClasse {
  final int id;
  final String descricao;
  final CnaeGrupo grupo;
  final List<CnaeSubclasse> subclasses;

  CnaeClasse(this.id, this.descricao, this.grupo, this.subclasses);
}
