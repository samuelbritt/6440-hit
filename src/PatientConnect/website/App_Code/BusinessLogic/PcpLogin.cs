
/// <summary>
/// Summary description for PCPLogin
/// </summary>
public class PcpLogin
{
    private PcpDAO pcpDAO;
	public PcpLogin()
	{
        pcpDAO = new PcpDAO();
	}

    public bool LoginIsValid(string username, string password)
    {
        return pcpDAO.isValidPcp(username, password);
    }
}