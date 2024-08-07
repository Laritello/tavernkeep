import { useSession } from './useSession';
import { useUserAccount } from './useUserAccount';

export const useCurrentUserAccount = () => {
    const session = useSession();

    return useUserAccount(session.userId);
};
