namespace TechnicalSupport.Client.Helpers;

public class GlobalFunctions
{
    public List<T> SearchEntitiesStatus<T>(List<T> originalEntities,
                                                  string searchText,
                                                  string selectedColumn,
                                                  string selectedSearchType,
                                                  string selectedDeleteFilter,
                                                  Func<T, bool> isDeletedPredicate = null)
    {
        // Start with a copy of the original list
        List<T> entities = new(originalEntities);

        if (isDeletedPredicate is not null)
        {
            // Apply deleted filter
            if (selectedDeleteFilter == Strings.Active)
            {
                entities = entities.Where(e => !isDeletedPredicate(e)).ToList();
            }
            else if (selectedDeleteFilter == Strings.Deleted)
            {
                entities = entities.Where(e => isDeletedPredicate(e)).ToList();
            }
        }

        // Exit if search text or selected column is null/empty
        if (searchText.IsEmpty() || selectedColumn.IsEmpty())
        {
            return entities;
        }

        // Apply search filter
        switch (selectedSearchType)
        {
            case Strings.Contains:
                entities = entities.Where(e => e?.GetType()
                                                 .GetProperty(selectedColumn)?
                                                 .GetValue(e, null)?
                                                 .ToString()?
                                                 .Contains(searchText, StringComparison.OrdinalIgnoreCase) == true)
                                                 .ToList();
                break;

            case Strings.NotContains:
                entities = entities.Where(e => e?.GetType()
                                                 .GetProperty(selectedColumn)?
                                                 .GetValue(e, null)?
                                                 .ToString()?
                                                 .Contains(searchText, StringComparison.OrdinalIgnoreCase) == false)
                                                 .ToList();
                break;

            case Strings.Equal:
                entities = entities.Where(e => e?.GetType()
                                                 .GetProperty(selectedColumn)?
                                                 .GetValue(e, null)?
                                                 .ToString()?
                                                 .Equals(searchText, StringComparison.OrdinalIgnoreCase) == true)
                                                 .ToList();
                break;

            case Strings.NotEqual:
                entities = entities.Where(e => e?.GetType()
                                                 .GetProperty(selectedColumn)?
                                                 .GetValue(e, null)?
                                                 .ToString()?
                                                 .Equals(searchText, StringComparison.OrdinalIgnoreCase) == false)
                                                 .ToList();
                break;

            case Strings.StartsWith:
                entities = entities.Where(e => e?.GetType()
                                                 .GetProperty(selectedColumn)?
                                                 .GetValue(e, null)?
                                                 .ToString()?
                                                 .StartsWith(searchText, StringComparison.OrdinalIgnoreCase) == true)
                                                 .ToList();
                break;

            case Strings.NotStartsWith:
                entities = entities.Where(e => e?.GetType()
                                                 .GetProperty(selectedColumn)?
                                                 .GetValue(e, null)?
                                                 .ToString()?
                                                 .StartsWith(searchText, StringComparison.OrdinalIgnoreCase) == false)
                                                 .ToList();
                break;

            case Strings.EndsWith:
                entities = entities.Where(e => e?.GetType()
                                                 .GetProperty(selectedColumn)?
                                                 .GetValue(e, null)?
                                                 .ToString()?
                                                 .EndsWith(searchText, StringComparison.OrdinalIgnoreCase) == true)
                                                 .ToList();
                break;

            case Strings.NotEndsWith:
                entities = entities.Where(e => e?.GetType()
                                                 .GetProperty(selectedColumn)?
                                                 .GetValue(e, null)?
                                                 .ToString()?
                                                 .EndsWith(searchText, StringComparison.OrdinalIgnoreCase) == false)
                                                 .ToList();
                break;
        }

        return entities;
    }
}
