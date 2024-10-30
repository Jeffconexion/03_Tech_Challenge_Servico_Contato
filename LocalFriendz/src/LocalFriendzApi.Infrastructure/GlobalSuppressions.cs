﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements should appear in the correct order", Justification = "<Pending>", Scope = "member", Target = "~F:LocalFriendzApi.Infrastructure.Logging.CustomLogger._loggerName")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:LocalFriendzApi.Infrastructure.Logging.CustomLogger.#ctor(System.String,LocalFriendzApi.Infrastructure.Logging.CustomLoggerProviderConfiguration)")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Pending>", Scope = "member", Target = "~F:LocalFriendzApi.Infrastructure.Logging.CustomLogger._loggerName")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:LocalFriendzApi.Infrastructure.Logging.CustomLogger.Log``1(Microsoft.Extensions.Logging.LogLevel,Microsoft.Extensions.Logging.EventId,``0,System.Exception,System.Func{``0,System.Exception,System.String})")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Pending>", Scope = "member", Target = "~F:LocalFriendzApi.Infrastructure.Logging.CustomLogger._loggerConfig")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Pending>", Scope = "member", Target = "~F:LocalFriendzApi.Infrastructure.Logging.CustomLoggerProvider._loggerConfiguration")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:LocalFriendzApi.Infrastructure.Logging.CustomLoggerProvider.CreateLogger(System.String)~Microsoft.Extensions.Logging.ILogger")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:LocalFriendzApi.Infrastructure.Repositories.ContactRepository.GetContactByCodeRegion(System.String)~System.Threading.Tasks.Task{System.Collections.Generic.IEnumerable{LocalFriendzApi.Domain.Models.Contact}}")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:LocalFriendzApi.Infrastructure.Repositories.ContactRepository.GetAllContactWithAreaCode~System.Threading.Tasks.Task{System.Collections.Generic.IEnumerable{LocalFriendzApi.Domain.Models.Contact}}")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:LocalFriendzApi.Infrastructure.Repositories.ContactRepository.GetContactByCodeRegion(System.String)~System.Collections.Generic.IEnumerable{LocalFriendzApi.Domain.Models.Contact}")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:LocalFriendzApi.Infrastructure.Repositories.ContactRepository.GetAllContactWithAreaCode~System.Collections.Generic.IEnumerable{LocalFriendzApi.Domain.Models.Contact}")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:LocalFriendzApi.Infrastructure.Repositories.Repository`1.#ctor(LocalFriendzApi.Infrastructure.Data.Context.AppDbContext)")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "<Pending>", Scope = "member", Target = "~F:LocalFriendzApi.Infrastructure.Repositories.Repository`1.Db")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:LocalFriendzApi.Infrastructure.Repositories.Repository`1.Add(`0)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "<Pending>", Scope = "member", Target = "~F:LocalFriendzApi.Infrastructure.Repositories.Repository`1.DbSet")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:LocalFriendzApi.Infrastructure.Repositories.Repository`1.Update(`0)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:LocalFriendzApi.Infrastructure.Repositories.Repository`1.Delete(System.Guid)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:LocalFriendzApi.Infrastructure.Repositories.Repository`1.SaveChanges~System.Threading.Tasks.Task{System.Int32}")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:LocalFriendzApi.Infrastructure.Repositories.Repository`1.GetAll~System.Threading.Tasks.Task{System.Collections.Generic.List{`0}}")]