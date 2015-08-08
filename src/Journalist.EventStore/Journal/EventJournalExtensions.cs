using System.Threading.Tasks;

namespace Journalist.EventStore.Journal
{
    public static class EventJournalExtensions
    {
        public static Task<IEventStreamCursor> OpenEventStreamCursorAsync(
            this IEventJournal journal,
            string streamName,
            StreamVersion fromVersion)
        {
            Require.NotNull(journal, "journal");

            return journal.OpenEventStreamCursorAsync(
                streamName,
                fromVersion,
                Constants.Settings.DEFAULT_EVENT_SLICE_SIZE);
        }

        public static Task<IEventStreamCursor> OpenEventStreamCursorAsync(
            this IEventJournal journal,
            string streamName)
        {
            Require.NotNull(journal, "journal");

            return journal.OpenEventStreamCursorAsync(
                streamName,
                Constants.Settings.DEFAULT_EVENT_SLICE_SIZE);
        }
    }
}
