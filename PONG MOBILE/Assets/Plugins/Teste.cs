using UnityEngine;
using SQLite;
using System.IO;

public class SQLiteNetManager : MonoBehaviour
{
    private SQLiteConnection db;

    void Start()
    {
        // Caminho do banco de dados
        string dbPath = Path.Combine(Application.persistentDataPath, "meubanco.db");
        
        // Criar conexão
        db = new SQLiteConnection(dbPath);

        // Criar tabela
        db.CreateTable<User>();

        // Inserir dados
        db.Insert(new User { Name = "João", Age = 25 });

        // Ler dados
        var users = db.Table<User>().ToList();
        foreach (var user in users)
        {
            Debug.Log($"ID: {user.Id}, Nome: {user.Name}, Idade: {user.Age}");
        }
    }
}

// Classe para a tabela User
public class User
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}