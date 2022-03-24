namespace TimesheetCalculator.Domain
{
    public class TimesheetTimeParser
    {
        public TimesheetTime Parse(string time)
        {
            if (string.IsNullOrWhiteSpace(time))
            {
                throw new ArgumentException($"'{nameof(time)}' cannot be null or whitespace.", nameof(time));
            }

            var hours = GetHours(time);
            var minutes = GetMinutes(time);

            if (hours < 0 || hours > 24)
            {
                throw new TimesheetTimeParsingException("Hours are out of range.");
            }

            if (minutes < 0 || minutes > 60)
            {
                throw new TimesheetTimeParsingException("Minutes are out of range.");
            }

            return new TimesheetTime(hours, minutes);
        }

        private int GetMinutes(string time)
        {
            var splitted = time.Split(':');

            if (splitted.Length != 2 || !int.TryParse(splitted[1], out int result))
            {
                throw new TimesheetTimeParsingException("Invalid format.");
            }

            return result;
        }

        private int GetHours(string time)
        {
            var splitted = time.Split(':');

            if (splitted.Length != 2 || !int.TryParse(splitted[0], out int result))
            {
                throw new TimesheetTimeParsingException("Invalid format.");
            }

            return result;
        }
    }
}