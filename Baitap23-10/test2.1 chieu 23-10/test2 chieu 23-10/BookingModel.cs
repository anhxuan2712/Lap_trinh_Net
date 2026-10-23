using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace test2_chieu_23_10
{
    public class BookingModel : INotifyPropertyChanged
    {
        private string bookingId;
        private string customerName;
        private string identityCard;
        private string roomType;
        private DateTime checkInDate;
        private int days;
        private decimal pricePerNight;

        public string BookingId
        {
            get => bookingId;
            set
            {
                if (bookingId != value)
                {
                    bookingId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CustomerName
        {
            get => customerName;
            set
            {
                if (customerName != value)
                {
                    customerName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string IdentityCard
        {
            get => identityCard;
            set
            {
                if (identityCard != value)
                {
                    identityCard = value;
                    OnPropertyChanged();
                }
            }
        }

        public string RoomType
        {
            get => roomType;
            set
            {
                if (roomType != value)
                {
                    roomType = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime CheckInDate
        {
            get => checkInDate;
            set
            {
                if (checkInDate != value)
                {
                    checkInDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Days
        {
            get => days;
            set
            {
                if (days != value)
                {
                    days = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal PricePerNight
        {
            get => pricePerNight;
            set
            {
                if (pricePerNight != value)
                {
                    pricePerNight = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
