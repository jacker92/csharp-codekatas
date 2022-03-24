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
                return int.TryParse(time.AsSpan(time.Length - 2), out var parsedMinutes) ?
                    parsedMinutes :
                    throw new TimesheetTimeParsingException();
            }

            var splitted = time.Split(':');

            if (splitted.Length != 2)
            {
                throw new TimesheetTimeParsingException();
            }

            var minutes = splitted[1];

            if (minutes.Length < 2 || !int.TryParse(minutes.Substring(0, 2), out int result))
            {
                throw new TimesheetTimeParsingException();
            }

            return result;
        }

        private int HandleAmericanFormat(string time, int result)
        {
            if (time.Contains("PM") && result == 12)
            {
                return result;
            }
            else if (time.Contains("PM"))
            {
                return 24 - (12 - result);
            }
            else if (result == 12)
            {
                return 0;
            }
            else if (result > 12)
            {
                throw new TimesheetTimeParsingException();
            }

            return result;
        }

        private int GetHours(string time)
        {
            if (time.Length == 3 || time.Length == 4)
            {
                return int.TryParse(time.AsSpan(0, time.Length - 2), out var hours) ?
                    hours :
                    throw new TimesheetTimeParsingException();
            }

            var splitted = time.Split(':');

            if (splitted.Length != 2 || !int.TryParse(splitted[0], out int result))
            {
                throw new TimesheetTimeParsingException("Invalid format.");
            }

            if (time.Contains("AM") || time.Contains("PM"))
            {
                return HandleAmericanFormat(time, result);
            }

            return result;
        }
    }
}