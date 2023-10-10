using System.Linq.Expressions;
using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace DocConnect.Data.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<ICollection<Appointment>> FilteredAppointmentsAsync(Expression<Func<Appointment, bool>> expression, string appointmentsType)
        {
            return await DbSet()
              .Where(expression)
               .Include(a => a.Doctor)
               .ThenInclude(d => d.Specialty)
               .Include(d => d.Doctor.Location)
               .Include(a => a.Patient)
               .ThenInclude(p => p.User)
               .ToListAsync();
        }

        public async Task<ICollection<Appointment>> AllPatientAppointmentsAsync(Expression<Func<Appointment, bool>> expression, string appointmentsType)
        {
            return await DbSet()
               .Where(expression)
               .Include(a => a.Doctor)
               .ThenInclude(d => d.Specialty)
               .Include(d => d.Doctor.Location)
               .Include(a => a.Patient)
               .ThenInclude(p => p.User)
               .ToListAsync();
        }

        public async Task<bool> IsDoctorAppointmentFreeAsync(int doctorId, DateTime scheduledTime)
        {
            return !await DbSet()
                .AnyAsync(a => a.DoctorId == doctorId && a.ScheduledTime == scheduledTime);
        }

        public async Task<bool> IsPatientAppointmentFreeAsync(int patientId, DateTime scheduledTime)
        {
            return !await DbSet()
                .AnyAsync(a => a.PatientId == patientId && a.ScheduledTime == scheduledTime);
        }

        public async Task<ICollection<DateTime>> GetTakenAppointmentHoursForDoctorAsync(int doctorId)
        {
            return await DbSet()
                .Where(a => a.DoctorId == doctorId)
                .Select(a => a.ScheduledTime)
                .ToListAsync();
        }
    }
}
