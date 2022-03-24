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

            time = time.Trim();

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
            if (time.Length == 3 || time.Length == 4)
            {
                return int.TryParse(time.AsSpan(time.Length - 2), out var minutes) ?
                    minutes :
                    throw new TimesheetTimeParsingException();
            }

            var splitted = time.Split(':');

            if (splitted.Length != 2 || !int.TryParse(splitted[1], out int result))
            {
                throw new TimesheetTimeParsingException();
            }

            return result;
        }

        private int GetHours(string time)
        {
            if (time.Length == 3 || time.Length == 4)
            {
                return int.TryParse(time.AsSpan(0,time.Length-2), out var hours) ?
                    hours :
                    throw new TimesheetTimeParsingException();
            }

            var splitted = time.Split(':');

            if (splitted.Length != 2 || !int.TryParse(splitted[0], out int result))
            {
                throw new TimesheetTimeParsingException("Invalid format.");
            }

            return result;
        }
    }
}