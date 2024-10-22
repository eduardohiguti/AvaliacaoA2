namespace Vitor_Campos_da_Silva.Models;

using System.ComponentModel.DataAnnotations;


public class Funcionario
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string? Nome { get; set; }
    
    public string? Cpf { get; set; }
    
    public ICollection<Folha> Folhas { get; set; }
}