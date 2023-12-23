using FluentValidation;

namespace FeaturesForMongoDB.Validators
{
    public class ConfigurationValidator : AbstractValidator<OptionsFile>
    {
        public ConfigurationValidator()
        {
            RuleFor(m => m.MongoSettings).NotNull().WithMessage($"Need connection settings of database. {nameof(MongoSettings)}");
            RuleFor(x => x).Custom((obj, context) =>
            {
                if ((obj.ImplementationsToCreate == null || obj.ImplementationsToCreate.Length == 0) &&
                (obj.ImplementationsToUpdate == null || obj.ImplementationsToUpdate.Length == 0))
                {
                    context.AddFailure($"Se necesita especificar almenos una implementación. {nameof(obj.ImplementationsToCreate)} / {nameof(obj.ImplementationsToUpdate)}");
                }
            });
            RuleFor(x => x).Custom((obj, context) =>
            {
                if (!obj.UpdateCollection && !obj.CreateJson)
                {
                    context.AddFailure($"Se necesita especificar si se quiere actualizar la base de datos o crear un json. {nameof(obj.UpdateCollection)} / {nameof(obj.CreateJson)}");
                }
            });
        }
    }
}
