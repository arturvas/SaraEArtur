using Wedding.API.Core.Models;
using Wedding.API.Data;

namespace Wedding.API.Data;

public static class GiftSeeder
{
    public static void Seed(AppDbContext db)
    {
        var giftsToSeed = new List<Gift>
        {
            new Gift { Category = "Utensílios da Cozinha", Title = "Kit copo e jarra", Price = 120, TimesTaken = 0 },
            new Gift { Category = "Utensílios da Cozinha", Title = "Travessa de vidro", Price = 80, TimesTaken = 0 },
            new Gift { Category = "Utensílios da Cozinha", Title = "Jogo de xícaras", Price = 142, TimesTaken = 0 },
            new Gift { Category = "Utensílios da Cozinha", Title = "Escorregador de louça", Price = 150, TimesTaken = 0 },
            new Gift { Category = "Utensílios da Cozinha", Title = "Panela de pressão", Price = 130, TimesTaken = 0 },
            new Gift { Category = "Utensílios da Cozinha", Title = "Jogo de panelas", Price = 400, TimesTaken = 0 },

            new Gift { Category = "Eletroportáteis", Title = "Liquidificador", Price = 170, TimesTaken = 0 },
            new Gift { Category = "Eletroportáteis", Title = "Ferro de passar", Price = 200, TimesTaken = 0 },
            new Gift { Category = "Eletroportáteis", Title = "Air Fryer", Price = 542, TimesTaken = 0 },
            new Gift { Category = "Eletroportáteis", Title = "Microondas", Price = 570, TimesTaken = 0 },
            new Gift { Category = "Eletroportáteis", Title = "Filtro de água", Price = 650 },
            new Gift { Category = "Eletroportáteis", Title = "Batedeira", Price = 250 },

            new Gift { Category = "Cama & Banho", Title = "Jogo de cama", Price = 350, TimesTaken = 0 },
            new Gift { Category = "Cama & Banho", Title = "Toalhas de banho", Price = 90, TimesTaken = 0 },
            new Gift { Category = "Cama & Banho", Title = "Par de travesseiros", Price = 150, TimesTaken = 0 },
            new Gift { Category = "Cama & Banho", Title = "Jogo de fronhas", Price = 70, TimesTaken = 0 },
            new Gift { Category = "Cama & Banho", Title = "Tapete pro banheiro", Price = 50, TimesTaken = 0 },
            new Gift { Category = "Cama & Banho", Title = "Edredom de casal", Price = 200, TimesTaken = 0 },

            new Gift { Category = "Casa & Utilidades", Title = "Jogo de tapetes", Price = 99, TimesTaken = 0 },
            new Gift { Category = "Casa & Utilidades", Title = "Fogão", Price = 900, TimesTaken = 0 },
            new Gift { Category = "Casa & Utilidades", Title = "Geladeira", Price = 2000, TimesTaken = 0 },

            new Gift { Category = "Mudança", Title = "Primeiro passo", Price = 100, TimesTaken = 0 },
            new Gift { Category = "Mudança", Title = "Mãos à obra", Price = 300, TimesTaken = 0 },
            new Gift { Category = "Mudança", Title = "Novo lar", Price = 500, TimesTaken = 0 },

            new Gift { Category = "Lua de mel", Title = "Descanso merecido", Price = 150, TimesTaken = 0 },
            new Gift { Category = "Lua de mel", Title = "Viagem a dois", Price = 400, TimesTaken = 0 },
            new Gift { Category = "Lua de mel", Title = "Sonho realizado", Price = 800, TimesTaken = 0 }
        };

        foreach (var gift in giftsToSeed)
        {
            if (!db.Gifts.Any(x => x.Title == gift.Title))
            {
                db.Gifts.Add(gift);
            }
        }

        db.SaveChanges();
        Console.WriteLine("Presentes atualizados com sucesso.");
    }
}
