using System;
using System.Timers;
using System.Net.Sockets;


namespace WebApplication4.Models
{

    enum ClientStatus { running, standby };

    /***
     * Simulator client get strings of requests, forward them to the simulator, and return the
     * answers in an array of doubles.
     * Connection stays open only if in use before the timer is over.
     * Upon creation, timer sets to default (5 seconds)
     * and upon requesting data, timer sets to after request timer (1 second)
     */
    public class SimulatorClient
    {

        static class Constants
        {
            public const int CreateTimer = 5;
            public const int AfterRequestTimer = 1;
            public const int DefaultBufferSize = 256;
        }

        #region connection members
        TcpClient _client;
        NetworkStream _stream;
        string _ip;
        int _port;
        #endregion

        Timer timer;
        ClientStatus _status;


        public SimulatorClient(string ip, int port)
        {
            this._client = new TcpClient();
            this.timer = new Timer();

            Start();
            SetTimer(Constants.CreateTimer);
        }

        public double[] GetData(string[] parameters)
        {
            if (_status == ClientStatus.standby)
            {
                Start();
            }

            double[] data = new double[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                Byte[] buffer = System.Text.Encoding.ASCII.GetBytes(parameters[i]);

                _stream.Write(buffer, 0, data.Length);

                buffer = new byte[Constants.DefaultBufferSize];
                string responseData = String.Empty;

                Int32 redBytes = _stream.Read(buffer, 0, buffer.Length);
                responseData = System.Text.Encoding.ASCII.GetString(buffer, 0, redBytes);

                data[i] = Double.Parse((responseData.Split('\''))[1]);
            }

            SetTimer(Constants.AfterRequestTimer);

            return data;
        }

        void SetTimer(int seconds)
        {
            if (timer.Enabled)
            {
                timer.Stop();
            }
            timer.Interval = (double)seconds * 1000;
            timer.Start();
        }

        void Start()
        {
            _client.Connect(_ip, _port);
            _stream = _client.GetStream();
            _status = ClientStatus.running;
        }

        void Stop(Object source, ElapsedEventArgs e)
        {
            _stream.Close();
            _stream.Dispose();
            _client.Close();
            _status = ClientStatus.standby;
        }
    }
}