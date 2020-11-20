using Liquid.Entities.Internal;

namespace Liquid.Entities.Internal {

public delegate void I<T0>(in T0 c0);
public delegate void II<T0, T1>(in T0 c0, in T1 c1);
public delegate void III<T0, T1, T2>(in T0 c0, in T1 c1, in T2 c2);
public delegate void IIII<T0, T1, T2, T3>(in T0 c0, in T1 c1, in T2 c2, in T3 c3);
public delegate void IIIII<T0, T1, T2, T3, T4>(in T0 c0, in T1 c1, in T2 c2, in T3 c3, in T4 c4);
public delegate void IIIIII<T0, T1, T2, T3, T4, T5>(in T0 c0, in T1 c1, in T2 c2, in T3 c3, in T4 c4, in T5 c5);
public delegate void R<T0>(ref T0 c0);
public delegate void RI<T0, T1>(ref T0 c0, in T1 c1);
public delegate void RII<T0, T1, T2>(ref T0 c0, in T1 c1, in T2 c2);
public delegate void RIII<T0, T1, T2, T3>(ref T0 c0, in T1 c1, in T2 c2, in T3 c3);
public delegate void RIIII<T0, T1, T2, T3, T4>(ref T0 c0, in T1 c1, in T2 c2, in T3 c3, in T4 c4);
public delegate void RIIIII<T0, T1, T2, T3, T4, T5>(ref T0 c0, in T1 c1, in T2 c2, in T3 c3, in T4 c4, in T5 c5);
public delegate void RR<T0, T1>(ref T0 c0, ref T1 c1);
public delegate void RRI<T0, T1, T2>(ref T0 c0, ref T1 c1, in T2 c2);
public delegate void RRII<T0, T1, T2, T3>(ref T0 c0, ref T1 c1, in T2 c2, in T3 c3);
public delegate void RRIII<T0, T1, T2, T3, T4>(ref T0 c0, ref T1 c1, in T2 c2, in T3 c3, in T4 c4);
public delegate void RRIIII<T0, T1, T2, T3, T4, T5>(ref T0 c0, ref T1 c1, in T2 c2, in T3 c3, in T4 c4, in T5 c5);
public delegate void RRR<T0, T1, T2>(ref T0 c0, ref T1 c1, ref T2 c2);
public delegate void RRRI<T0, T1, T2, T3>(ref T0 c0, ref T1 c1, ref T2 c2, in T3 c3);
public delegate void RRRII<T0, T1, T2, T3, T4>(ref T0 c0, ref T1 c1, ref T2 c2, in T3 c3, in T4 c4);
public delegate void RRRIII<T0, T1, T2, T3, T4, T5>(ref T0 c0, ref T1 c1, ref T2 c2, in T3 c3, in T4 c4, in T5 c5);
public delegate void RRRR<T0, T1, T2, T3>(ref T0 c0, ref T1 c1, ref T2 c2, ref T3 c3);
public delegate void RRRRI<T0, T1, T2, T3, T4>(ref T0 c0, ref T1 c1, ref T2 c2, ref T3 c3, in T4 c4);
public delegate void RRRRII<T0, T1, T2, T3, T4, T5>(ref T0 c0, ref T1 c1, ref T2 c2, ref T3 c3, in T4 c4, in T5 c5);
public delegate void RRRRR<T0, T1, T2, T3, T4>(ref T0 c0, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4);
public delegate void RRRRRI<T0, T1, T2, T3, T4, T5>(ref T0 c0, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, in T5 c5);
public delegate void RRRRRR<T0, T1, T2, T3, T4, T5>(ref T0 c0, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5);
public delegate void V<T0>(T0 c0);
public delegate void VI<T0, T1>(T0 c0, in T1 c1);
public delegate void VII<T0, T1, T2>(T0 c0, in T1 c1, in T2 c2);
public delegate void VIII<T0, T1, T2, T3>(T0 c0, in T1 c1, in T2 c2, in T3 c3);
public delegate void VIIII<T0, T1, T2, T3, T4>(T0 c0, in T1 c1, in T2 c2, in T3 c3, in T4 c4);
public delegate void VIIIII<T0, T1, T2, T3, T4, T5>(T0 c0, in T1 c1, in T2 c2, in T3 c3, in T4 c4, in T5 c5);
public delegate void VR<T0, T1>(T0 c0, ref T1 c1);
public delegate void VRI<T0, T1, T2>(T0 c0, ref T1 c1, in T2 c2);
public delegate void VRII<T0, T1, T2, T3>(T0 c0, ref T1 c1, in T2 c2, in T3 c3);
public delegate void VRIII<T0, T1, T2, T3, T4>(T0 c0, ref T1 c1, in T2 c2, in T3 c3, in T4 c4);
public delegate void VRIIII<T0, T1, T2, T3, T4, T5>(T0 c0, ref T1 c1, in T2 c2, in T3 c3, in T4 c4, in T5 c5);
public delegate void VRR<T0, T1, T2>(T0 c0, ref T1 c1, ref T2 c2);
public delegate void VRRI<T0, T1, T2, T3>(T0 c0, ref T1 c1, ref T2 c2, in T3 c3);
public delegate void VRRII<T0, T1, T2, T3, T4>(T0 c0, ref T1 c1, ref T2 c2, in T3 c3, in T4 c4);
public delegate void VRRIII<T0, T1, T2, T3, T4, T5>(T0 c0, ref T1 c1, ref T2 c2, in T3 c3, in T4 c4, in T5 c5);
public delegate void VRRR<T0, T1, T2, T3>(T0 c0, ref T1 c1, ref T2 c2, ref T3 c3);
public delegate void VRRRI<T0, T1, T2, T3, T4>(T0 c0, ref T1 c1, ref T2 c2, ref T3 c3, in T4 c4);
public delegate void VRRRII<T0, T1, T2, T3, T4, T5>(T0 c0, ref T1 c1, ref T2 c2, ref T3 c3, in T4 c4, in T5 c5);
public delegate void VRRRR<T0, T1, T2, T3, T4>(T0 c0, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4);
public delegate void VRRRRI<T0, T1, T2, T3, T4, T5>(T0 c0, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, in T5 c5);
public delegate void VRRRRR<T0, T1, T2, T3, T4, T5>(T0 c0, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5);
public delegate void VV<T0, T1>(T0 c0, T1 c1);
public delegate void VVI<T0, T1, T2>(T0 c0, T1 c1, in T2 c2);
public delegate void VVII<T0, T1, T2, T3>(T0 c0, T1 c1, in T2 c2, in T3 c3);
public delegate void VVIII<T0, T1, T2, T3, T4>(T0 c0, T1 c1, in T2 c2, in T3 c3, in T4 c4);
public delegate void VVIIII<T0, T1, T2, T3, T4, T5>(T0 c0, T1 c1, in T2 c2, in T3 c3, in T4 c4, in T5 c5);
public delegate void VVR<T0, T1, T2>(T0 c0, T1 c1, ref T2 c2);
public delegate void VVRI<T0, T1, T2, T3>(T0 c0, T1 c1, ref T2 c2, in T3 c3);
public delegate void VVRII<T0, T1, T2, T3, T4>(T0 c0, T1 c1, ref T2 c2, in T3 c3, in T4 c4);
public delegate void VVRIII<T0, T1, T2, T3, T4, T5>(T0 c0, T1 c1, ref T2 c2, in T3 c3, in T4 c4, in T5 c5);
public delegate void VVRR<T0, T1, T2, T3>(T0 c0, T1 c1, ref T2 c2, ref T3 c3);
public delegate void VVRRI<T0, T1, T2, T3, T4>(T0 c0, T1 c1, ref T2 c2, ref T3 c3, in T4 c4);
public delegate void VVRRII<T0, T1, T2, T3, T4, T5>(T0 c0, T1 c1, ref T2 c2, ref T3 c3, in T4 c4, in T5 c5);
public delegate void VVRRR<T0, T1, T2, T3, T4>(T0 c0, T1 c1, ref T2 c2, ref T3 c3, ref T4 c4);
public delegate void VVRRRI<T0, T1, T2, T3, T4, T5>(T0 c0, T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, in T5 c5);
public delegate void VVRRRR<T0, T1, T2, T3, T4, T5>(T0 c0, T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5);
public delegate void VVV<T0, T1, T2>(T0 c0, T1 c1, T2 c2);
public delegate void VVVI<T0, T1, T2, T3>(T0 c0, T1 c1, T2 c2, in T3 c3);
public delegate void VVVII<T0, T1, T2, T3, T4>(T0 c0, T1 c1, T2 c2, in T3 c3, in T4 c4);
public delegate void VVVIII<T0, T1, T2, T3, T4, T5>(T0 c0, T1 c1, T2 c2, in T3 c3, in T4 c4, in T5 c5);
public delegate void VVVR<T0, T1, T2, T3>(T0 c0, T1 c1, T2 c2, ref T3 c3);
public delegate void VVVRI<T0, T1, T2, T3, T4>(T0 c0, T1 c1, T2 c2, ref T3 c3, in T4 c4);
public delegate void VVVRII<T0, T1, T2, T3, T4, T5>(T0 c0, T1 c1, T2 c2, ref T3 c3, in T4 c4, in T5 c5);
public delegate void VVVRR<T0, T1, T2, T3, T4>(T0 c0, T1 c1, T2 c2, ref T3 c3, ref T4 c4);
public delegate void VVVRRI<T0, T1, T2, T3, T4, T5>(T0 c0, T1 c1, T2 c2, ref T3 c3, ref T4 c4, in T5 c5);
public delegate void VVVRRR<T0, T1, T2, T3, T4, T5>(T0 c0, T1 c1, T2 c2, ref T3 c3, ref T4 c4, ref T5 c5);
public delegate void VVVV<T0, T1, T2, T3>(T0 c0, T1 c1, T2 c2, T3 c3);
public delegate void VVVVI<T0, T1, T2, T3, T4>(T0 c0, T1 c1, T2 c2, T3 c3, in T4 c4);
public delegate void VVVVII<T0, T1, T2, T3, T4, T5>(T0 c0, T1 c1, T2 c2, T3 c3, in T4 c4, in T5 c5);
public delegate void VVVVR<T0, T1, T2, T3, T4>(T0 c0, T1 c1, T2 c2, T3 c3, ref T4 c4);
public delegate void VVVVRI<T0, T1, T2, T3, T4, T5>(T0 c0, T1 c1, T2 c2, T3 c3, ref T4 c4, in T5 c5);
public delegate void VVVVRR<T0, T1, T2, T3, T4, T5>(T0 c0, T1 c1, T2 c2, T3 c3, ref T4 c4, ref T5 c5);
public delegate void VVVVV<T0, T1, T2, T3, T4>(T0 c0, T1 c1, T2 c2, T3 c3, T4 c4);
public delegate void VVVVVI<T0, T1, T2, T3, T4, T5>(T0 c0, T1 c1, T2 c2, T3 c3, T4 c4, in T5 c5);
public delegate void VVVVVR<T0, T1, T2, T3, T4, T5>(T0 c0, T1 c1, T2 c2, T3 c3, T4 c4, ref T5 c5);
public delegate void VVVVVV<T0, T1, T2, T3, T4, T5>(T0 c0, T1 c1, T2 c2, T3 c3, T4 c4, T5 c5);

} // namespace Liquid.Entities.Internal

namespace Liquid.Entities {

public partial class World {
    public void Each<T0>(
        V<T0> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0>(includeInactive)) {
            fn(Unpack<T0>(entity));
        }
    }

    public void Each<T0>(
        R<T0> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0>(includeInactive)) {
            fn(ref Unpack<T0>(entity));
        }
    }

    public void Each<T0>(
        I<T0> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0>(includeInactive)) {
            fn(in Unpack<T0>(entity));
        }
    }

    public void Each<T0, T1>(
        VR<T0, T1> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1>(includeInactive)) {
            fn(Unpack<T0>(entity),
               ref Unpack<T1>(entity));
        }
    }

    public void Each<T0, T1>(
        RI<T0, T1> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               in Unpack<T1>(entity));
        }
    }

    public void Each<T0, T1>(
        VV<T0, T1> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity));
        }
    }

    public void Each<T0, T1>(
        RR<T0, T1> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               ref Unpack<T1>(entity));
        }
    }

    public void Each<T0, T1>(
        VI<T0, T1> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1>(includeInactive)) {
            fn(Unpack<T0>(entity),
               in Unpack<T1>(entity));
        }
    }

    public void Each<T0, T1>(
        II<T0, T1> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1>(includeInactive)) {
            fn(in Unpack<T0>(entity),
               in Unpack<T1>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        III<T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(in Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        VVV<T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        RRI<T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               in Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        RII<T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        VVI<T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               in Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        VRI<T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               in Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        VII<T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        VVR<T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               ref Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        RRR<T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        VRR<T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VVVR<T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               ref Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VRII<T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VVII<T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VRRR<T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VVRI<T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VVVV<T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VIII<T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        IIII<T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(in Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        RIII<T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        RRRI<T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        RRII<T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VVVI<T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VVRR<T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        RRRR<T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VRRI<T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVVRI<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVVVV<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               Unpack<T3>(entity),
               Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VRRRR<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               ref Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        RRRRR<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               ref Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVRRR<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               ref Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VRRII<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        RIIII<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVVRR<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               ref Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        RRRII<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVVII<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVVVI<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVRII<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        IIIII<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(in Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVIII<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VIIII<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVVVR<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               Unpack<T3>(entity),
               ref Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VRRRI<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        RRRRI<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        RRIII<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VRIII<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVRRI<T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VVVIII<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        RIIIII<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VRRIII<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VVRRRI<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               ref Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VVVVRI<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               Unpack<T3>(entity),
               ref Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VRRRRR<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               ref Unpack<T4>(entity),
               ref Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VRRRII<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               in Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VVVRII<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               in Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VVVVVV<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               Unpack<T3>(entity),
               Unpack<T4>(entity),
               Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        RRRRRI<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               ref Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        RRRIII<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VVVVRR<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               Unpack<T3>(entity),
               ref Unpack<T4>(entity),
               ref Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VVRRRR<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               ref Unpack<T4>(entity),
               ref Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VIIIII<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VVRRII<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               in Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        IIIIII<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(in Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VVVRRI<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               ref Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VVRIII<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VVVVVI<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               Unpack<T3>(entity),
               Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        RRRRRR<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               ref Unpack<T4>(entity),
               ref Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VVVVII<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               Unpack<T3>(entity),
               in Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VRRRRI<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               ref Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VRIIII<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VVVRRR<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               ref Unpack<T4>(entity),
               ref Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        RRRRII<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               in Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        RRIIII<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VVVVVR<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               Unpack<T3>(entity),
               Unpack<T4>(entity),
               ref Unpack<T5>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4, T5>(
        VVIIII<T0, T1, T2, T3, T4, T5> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4, T5>(includeInactive)) {
            fn(Unpack<T0>(entity),
               Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity),
               in Unpack<T5>(entity));
        }
    }

    public void Each(
        V<Entity> fn,
        bool includeInactive = false) {

        foreach (var entity in View(includeInactive)) {
            fn(entity);
        }
    }


    public void Each<T0>(
        VV<Entity, T0> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity));
        }
    }

    public void Each<T0>(
        VR<Entity, T0> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0>(includeInactive)) {
            fn(entity,
               ref Unpack<T0>(entity));
        }
    }

    public void Each<T0>(
        VI<Entity, T0> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0>(includeInactive)) {
            fn(entity,
               in Unpack<T0>(entity));
        }
    }

    public void Each<T0, T1>(
        VVR<Entity, T0, T1> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               ref Unpack<T1>(entity));
        }
    }

    public void Each<T0, T1>(
        VRI<Entity, T0, T1> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1>(includeInactive)) {
            fn(entity,
               ref Unpack<T0>(entity),
               in Unpack<T1>(entity));
        }
    }

    public void Each<T0, T1>(
        VVV<Entity, T0, T1> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity));
        }
    }

    public void Each<T0, T1>(
        VRR<Entity, T0, T1> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1>(includeInactive)) {
            fn(entity,
               ref Unpack<T0>(entity),
               ref Unpack<T1>(entity));
        }
    }

    public void Each<T0, T1>(
        VVI<Entity, T0, T1> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               in Unpack<T1>(entity));
        }
    }

    public void Each<T0, T1>(
        VII<Entity, T0, T1> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1>(includeInactive)) {
            fn(entity,
               in Unpack<T0>(entity),
               in Unpack<T1>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        VIII<Entity, T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(entity,
               in Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        VVVV<Entity, T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        VRRI<Entity, T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(entity,
               ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               in Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        VRII<Entity, T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(entity,
               ref Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        VVVI<Entity, T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               in Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        VVRI<Entity, T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               in Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        VVII<Entity, T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        VVVR<Entity, T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               ref Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        VRRR<Entity, T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(entity,
               ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2>(
        VVRR<Entity, T0, T1, T2> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VVVVR<Entity, T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               ref Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VVRII<Entity, T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VVVII<Entity, T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VVRRR<Entity, T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VVVRI<Entity, T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VVVVV<Entity, T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VVIII<Entity, T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VIIII<Entity, T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(entity,
               in Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VRIII<Entity, T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(entity,
               ref Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VRRRI<Entity, T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(entity,
               ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VRRII<Entity, T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(entity,
               ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VVVVI<Entity, T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VVVRR<Entity, T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VRRRR<Entity, T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(entity,
               ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3>(
        VVRRI<Entity, T0, T1, T2, T3> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               in Unpack<T3>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVVVRI<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVVVVV<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               Unpack<T3>(entity),
               Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVRRRR<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               ref Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VRRRRR<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               ref Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVVRRR<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               ref Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVRRII<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VRIIII<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               ref Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVVVRR<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               ref Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VRRRII<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVVVII<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVVVVI<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVVRII<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VIIIII<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               in Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVVIII<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVIIII<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               in Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVVVVR<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               Unpack<T2>(entity),
               Unpack<T3>(entity),
               ref Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVRRRI<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VRRRRI<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VRRIII<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               ref Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVRIII<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               ref Unpack<T1>(entity),
               in Unpack<T2>(entity),
               in Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }

    public void Each<T0, T1, T2, T3, T4>(
        VVVRRI<Entity, T0, T1, T2, T3, T4> fn,
        bool includeInactive = false) {

        foreach (var entity in View<T0, T1, T2, T3, T4>(includeInactive)) {
            fn(entity,
               Unpack<T0>(entity),
               Unpack<T1>(entity),
               ref Unpack<T2>(entity),
               ref Unpack<T3>(entity),
               in Unpack<T4>(entity));
        }
    }
}

} // namespace Liquid.Entities
