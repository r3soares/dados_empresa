import 'cnae_grupo.dart';
import 'cnae_secao.dart';

class CnaeDivisao {
  final int id;
  final String descricao;
  final Cnaesecao secao;
  final List<CnaeGrupo> grupos;

  CnaeDivisao(this.id, this.descricao, this.secao, this.grupos);
}
