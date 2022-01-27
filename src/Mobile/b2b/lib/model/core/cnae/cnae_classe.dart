import 'cnae_grupo.dart';
import 'cnae_subclasse.dart';

class CnaeClasse {
  final int id;
  final String descricao;
  final int codGrupo;
  final List<CnaeSubclasse> subclasses;

  CnaeClasse(this.id, this.descricao, this.codGrupo, this.subclasses);
}
