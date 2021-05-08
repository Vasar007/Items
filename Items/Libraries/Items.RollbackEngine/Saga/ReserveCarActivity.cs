using System;
using Items.Common.Logging;

namespace Items.RollbackEngine.Saga
{
    internal sealed class ReserveCarActivity : Activity
    {
        private static readonly ILogger Logger =
            LoggerFactory.CreateLoggerFor<ReserveCarActivity>();

        private static readonly Random Rnd = new Random(2);

        public override Uri WorkItemQueueAddress => new Uri("sb://./carReservations");

        public override Uri CompensationQueueAddress => new Uri("sb://./carCancellactions");


        public ReserveCarActivity()
        {
        }

        public override WorkLog DoWork(WorkItem workItem)
        {
            Logger.Message("Reserving car");

            object car = workItem.Arguments["vehicleType"];
            int reservationId = Rnd.Next(100000);

            Logger.Message($"Reserved car {reservationId.ToString()}.");

            return new WorkLog(this, new WorkResult { { "reservationId", reservationId } });
        }

        public override bool Compensate(WorkLog item, RoutingSlip routingSlip)
        {
            object reservationId = item.Result["reservationId"];

            Logger.Message($"Cancelled car {reservationId}.");

            return true;
        }
    }
}
