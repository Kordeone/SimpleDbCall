using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ExTools.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public WalletController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        [HttpPost("IncreaseStore")]
        public async Task<ActionResult> IncreaseWalletStore(WalletStoreParam param)
        {
            DynamicParameters parameters = new();
            parameters.Add("@Username", param.Username);
            parameters.Add("@CoinSymbol", param.Coinsymbol.ToUpper());
            parameters.Add("@Amount", param.Amount);

            await _dbConnection.QueryAsync("Tools_Wallet_IncreaseRemainAmount", parameters, commandType: CommandType.StoredProcedure);
            return Ok($"{param.Coinsymbol} Increased");
        }
        [HttpPost("IncreaseAllCoinsStore")]
        public async Task<ActionResult> IncreaseAllWalletStore(WalletStoreParam param)
        {
            DynamicParameters parameters = new();
            parameters.Add("@Username", param.Username);
            parameters.Add("@Amount", param.Amount);

            await _dbConnection.QueryAsync("Tools_Wallet_IncreaseRemainAmountAll", parameters, commandType: CommandType.StoredProcedure);
            return Ok("All Increased");
        }
        [HttpGet("UpdateRemainAmount")]
        public async Task<ActionResult> Ramin()
        {
            await _dbConnection.QueryAsync("UpdateRaminAmount", commandType: CommandType.StoredProcedure);
            return Ok("All Increased");
        }
    }
}
