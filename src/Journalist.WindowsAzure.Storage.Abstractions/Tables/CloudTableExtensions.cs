using Journalist.Collections;
using Journalist.Extensions;

namespace Journalist.WindowsAzure.Storage.Tables
{
    public static class CloudTableExtensions
    {
        public static ICloudTableEntityRangeQuery PrepareEntityRangeQueryByPartition(
            this ICloudTable table,
            string partitionKey,
            string[] properties)
        {
            Require.NotNull(table, "table");
            Require.NotNull(partitionKey, "partitionKey");

            return table.PrepareEntityFilterRangeQuery(
                "PartitionKey eq '{0}'".FormatString(partitionKey),
                properties);
        }

        public static ICloudTableEntityRangeQuery PrepareEntityRangeQueryByPartition(
            this ICloudTable table,
            string partitionKey)
        {
            return table.PrepareEntityRangeQueryByPartition(partitionKey, EmptyArray.Get<string>());
        }

        public static ICloudTableEntityRangeQuery PrepareEntityRangeQueryByRows(
            this ICloudTable table,
            string partitionKey,
            string fromRowKey,
            string toRowKey,
            string[] properties)
        {
            Require.NotNull(table, "table");
            Require.NotNull(fromRowKey, "fromRowKey");
            Require.NotNull(toRowKey, "toRowKey");

            return table.PrepareEntityFilterRangeQuery(
                "(PartitionKey eq '{0}') and (RowKey ge '{1}' and RowKey le '{2}')".FormatString(partitionKey,
                    fromRowKey, toRowKey),
                properties);
        }

        public static ICloudTableEntityRangeQuery PrepareEntityRangeQueryByRows(
            this ICloudTable table,
            string partitionKey,
            string fromRowKey,
            string toRowKey)
        {
            return table.PrepareEntityRangeQueryByRows(partitionKey, fromRowKey, toRowKey, EmptyArray.Get<string>());
        }

        public static ICloudTableEntitySegmentedRangeQuery PrepareEntitySegmentedRangeQueryByPartition(
            this ICloudTable table,
            string partitionKey,
            string[] properties)
        {
            Require.NotNull(table, "table");
            Require.NotNull(partitionKey, "partitionKey");

            return table.PrepareEntityFilterSegmentedRangeQuery(
                "PartitionKey eq '{0}'".FormatString(partitionKey),
                properties);
        }

        public static ICloudTableEntitySegmentedRangeQuery PrepareEntitySegmentedRangeQueryByPartition(
            this ICloudTable table,
            string partitionKey)
        {
            return table.PrepareEntitySegmentedRangeQueryByPartition(partitionKey, EmptyArray.Get<string>());
        }

        public static ICloudTableEntitySegmentedRangeQuery PrepareEntitySegmentedRangeQueryByRows(
            this ICloudTable table,
            string partitionKey,
            string fromRowKey,
            string toRowKey,
            string[] properties)
        {
            Require.NotNull(table, "table");
            Require.NotNull(fromRowKey, "fromRowKey");
            Require.NotNull(toRowKey, "toRowKey");

            return table.PrepareEntityFilterSegmentedRangeQuery(
                "(PartitionKey eq '{0}') and (RowKey ge '{1}' and RowKey le '{2}')".FormatString(partitionKey,
                    fromRowKey, toRowKey),
                properties);
        }

        public static ICloudTableEntitySegmentedRangeQuery PrepareEntitySegmentedRangeQueryByRows(
            this ICloudTable table,
            string partitionKey,
            string fromRowKey,
            string toRowKey)
        {
            return table.PrepareEntitySegmentedRangeQueryByRows(partitionKey, fromRowKey, toRowKey, EmptyArray.Get<string>());
        }
    }
}