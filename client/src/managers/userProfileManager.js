export const getUserProfiles = () => fetch("/api/userprofiles").then(res => res.json());
