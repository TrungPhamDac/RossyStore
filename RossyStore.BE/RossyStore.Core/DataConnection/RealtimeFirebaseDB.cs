using Firebase.Database;
using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.DataConnection
{
    public class RealtimeFirebaseDB
    {
        static IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "QS1djr7QKRkiMiDGVL0WZa0ddSWTlQJYHw0qDaiI",
            BasePath = "https://shoestore-data-default-rtdb.europe-west1.firebasedatabase.app/"
        };

        public IFirebaseClient Client = new FireSharp.FirebaseClient(config);

        public FirebaseClient FirebaseClient = new FirebaseClient(config.BasePath);
    }
}
