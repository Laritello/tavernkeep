import { ParticipantType } from '@/contracts/enums';

export interface Participant {
    id: string;
    entityId: string;
    type: ParticipantType;
    initiative: number | null;
}
