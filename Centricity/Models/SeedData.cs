using Centricity.Data;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace Centricity.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CentricityContext(serviceProvider.GetRequiredService<DbContextOptions<CentricityContext>>()))
            {
                if (context.Flow.Any())
                {
                    return;
                }

                var flow1 = new Flow();
                flow1.Name = "Seeded Flow 01";

                flow1.Steps = new List<Step>();

                var step1 = new Step();

                step1.Name = "Step 01";
                step1.Ordinal = 1;

                step1.Evidence = new List<Evidence>();
                step1.Transitions = new List<Transition>();

                step1.Evidence.Add(new Evidence() { Name = "Evidence 01 [Text]", EvidenceType = EvidenceType.TextInput });
                step1.Evidence.Add(new Evidence() { Name = "Evidence 02 [Checkbox]", EvidenceType = EvidenceType.Checkbox });

                step1.Transitions.Add(new Transition() { Name = "Next", IsEnd = false });

                flow1.Steps.Add(step1);

                var step2 = new Step();
                step2.Name = "Step 02";
                step2.Ordinal = 2;

                step2.Evidence = new List<Evidence>();
                step2.Transitions = new List<Transition>();

                step2.Evidence.Add(new Evidence() { Name = "Evidence 01 [Text]", EvidenceType = EvidenceType.TextInput });
                step2.Evidence.Add(new Evidence() { Name = "Evidence 02 [Text]", EvidenceType = EvidenceType.TextInput });
                step2.Evidence.Add(new Evidence() { Name = "Evidence 03 [Checkbox]", EvidenceType = EvidenceType.Checkbox });

                step2.Transitions.Add(new Transition() { Name = "End", IsEnd = true });

                flow1.Steps.Add(step1);
                flow1.Steps.Add(step2);

                context.Flow.Add(flow1);
                
                context.SaveChanges();


            }
        }
    }
}
