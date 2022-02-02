abstract class IDatabase {
  getById(id);
  getAll();
  find(instrucao, termo);
  find2(instrucao, termo);
  save(data);
  saveAll(data);
  update(data);
  delete(id);
}
