using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalR.Models;
using System.Threading;

namespace SignalR.Hubs
{
    public class TicTacToeHub : Hub
    {
        int y = (new Random()).Next(999);

        // Called by a client to start or join a game.
        // - id  Id of the game
        public void Join(int id)
        {
            var x = y;

            var game = (TicTacToeGame.Games.ContainsKey(id)) 
                ? TicTacToeGame.Games[id] 
                : null;

            if ((game == null) || (game.Status == TicTacToeGame.GameStatus.Ended))
            {   // start a new game and wait for opponent
                game = new TicTacToeGame(id, Context.ConnectionId);
                TicTacToeGame.Games[id] = game;
                Clients.Caller.join(id, 'X', ' ');
            }
            else if (game.Status == TicTacToeGame.GameStatus.Initialized)
            {   // join a game and prompt the waiting client
                var ch = game.Join(Context.ConnectionId);
                Clients.Caller.join(id, ch, game.Next);
                Clients.Client(game.ConnectionX).prompt(id, game.Next);
            }
        }

        // Called by a client when playing, i.e. setting a field.
        // - id  Id of the game
        // - x   X-Position of the field to set
        // - y   Y-Position of the field to set
        // - ch  Character to set
        public void Play(int id, int x, int y, char ch)
        {
            var game = (TicTacToeGame.Games.ContainsKey(id))
                ? TicTacToeGame.Games[id]
                : null;

            if (game != null)
            {
                game.SetField(x, y, ch);
                Clients.All.setField(id, x, y, ch);
                if (game.Status == TicTacToeGame.GameStatus.Running)
                {
                    Clients.Client(game.ConnectionO).prompt(id, game.Next);
                    Clients.Client(game.ConnectionX).prompt(id, game.Next);
                }
                else
                {
                    Clients.Client(game.ConnectionO).ended(id, game.Winner);
                    Clients.Client(game.ConnectionX).ended(id, game.Winner);
                }
            }
        }
    }
}