using System.Text;
using PurpleOrchid.Common.Extensions;
using PurpleOrchid.Common.Contracts;

namespace PurpleOrchid.Common.CommandLine
{
    public interface IArgumentParser
    {
        string GetArgumentValue(string argumentName);
        IDictionary<string, string> GetAllArguments();
    }

    public class ArgumentParser : IArgumentParser
    {
        private readonly IDictionary<string, string> _parsedArguments;

        public ArgumentParser(string[] args)
        {
            Require.NotNull(nameof(args), args);
            Require.NotEmpty(nameof(args), args);

            _parsedArguments = ParseArguments(args);
        }

        private static IDictionary<string, string> ParseArguments(IEnumerable<string> args)
        {
            var dictionary = new Dictionary<string, string>();
            var errors = new List<string>();

            foreach (var value in args)
            {
                var argument = value.Replace("--", "").Split('=');

                if (argument.Length != 2)
                {
                    errors.Add($"Argument {value} is invalid.");
                    continue;
                }

                dictionary.Add(argument[0].ToLower(), argument[1]);
            }

            CheckForErrors(errors);

            return dictionary;
        }

        public string GetArgumentValue(string argumentName)
        {
            if (argumentName.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(argumentName));
            }

            var value = _parsedArguments[argumentName.ToLower()];

            if (value.IsNullOrWhiteSpace())
            {
                throw new InvalidOperationException($"Argument name {argumentName} does not have a value.");
            }

            return value;
        }

        private static void CheckForErrors(IReadOnlyCollection<string> errors)
        {
            if (!errors.Any())
            {
                return;
            }

            var sb = new StringBuilder();

            foreach (var error in errors)
            {
                sb.AppendLine(error);
            }

            throw new InvalidOperationException(sb.ToString());
        }

        public IDictionary<string, string> GetAllArguments()
        {
            return _parsedArguments;
        }
    }
}
