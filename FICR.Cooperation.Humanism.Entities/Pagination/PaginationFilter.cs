
using FICR.Cooperation.Humanism.CrossCutting.Extensions;
using FICR.Cooperation.Humanism.CrossCutting.Wrapper;

namespace FICR.Cooperation.Humanism.Entities.Pagination
{
    public class PaginationFilter
    {
        private string _sort;
        public int PageSize { get; set; } = 10;  // Valor padrão para PageSize
        public int PageNumber { get; set; } = 0;  // Valor padrão para PageNumber
        public string Sort
        {
            get => _sort;
            set => ValidateValue(string.IsNullOrEmpty(value) ? "ASC" : value);
        }

        private void ValidateValue(object value)
        {
            var stringEnum = value?.ToString()?.ToUpper();
            if (!string.IsNullOrEmpty(stringEnum))
            {
                OrderTypeEnum convertedValue;
                bool tried = Enum.TryParse(stringEnum, out convertedValue);
                if (tried)
                {
                    _sort = convertedValue.GetEnumDescription();
                }
                else
                {
                    throw new ManagerException("Parametro de ordenação inválido, deve ser ASC ou DESC");
                }
            }
            else
            {
                _sort = "";
            }

        }
    }
}
