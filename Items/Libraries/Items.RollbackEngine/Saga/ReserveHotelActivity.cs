using System;
using Items.Common.Logging;

namespace Items.RollbackEngine.Saga
{
    internal sealed class ReserveHotelActivity : Activity
    {
        private static readonly ILogger Logger =
            LoggerFactory.CreateLoggerFor<ReserveHotelActivity>();

        private static readonly Random Rnd = new Random(1);

        public override Uri WorkItemQueueAddress => new Uri("sb://./hotelReservations");

        public override Uri CompensationQueueAddress => new Uri("sb://./hotelCancellations");


        public ReserveHotelActivity()
        {
        }

        public override WorkLog DoWork(WorkItem workItem)
        {
            Logger.Message("Reserving hotel");

            object car = workItem.Arguments["roomType"];
            int reservationId = Rnd.Next(100000);

            Logger.Message($"Reserved hotel {reservationId.ToString()}.");

            return new WorkLog(this, new WorkResult { { "reservationId", reservationId } });
        }

        public override bool Compensate(WorkLog item, RoutingSlip routingSlip)
        {
            object reservationId = item.Result["reservationId"];

            Logger.Message($"Cancelled hotel {reservationId}.");

            return true;
        }
    }
}
