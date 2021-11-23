using System;
using System.ComponentModel.DataAnnotations;

namespace MimicApp_Api.Models
{
    public class Palavra
    {
        [Key]
        public int  Id { get; set; }
        
        [MaxLength(20)]
        public string Nome  { get; set; }
        public int  Pontuacao { get; set; }
        public bool Ativo { get; set; }
        public DateTime Criacao { get; set; }
        public DateTime? Atualizado { get; set; }
    }
}