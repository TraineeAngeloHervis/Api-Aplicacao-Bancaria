namespace Infra.Data.Repository;

public class ContaRepository
{
    private readonly AppDbContext _context;
    
    public ContaRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public void AdicionarConta()
    {
        
    }
}