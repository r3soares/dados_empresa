import 'cnae_grupo.dart';
import 'cnae_secao.dart';

class CnaeDivisao {
  final int id;
  final String descricao;
  final String codSecao;
  final List<CnaeGrupo> grupos;

  CnaeDivisao(this.id, this.descricao, this.codSecao, this.grupos);
}
